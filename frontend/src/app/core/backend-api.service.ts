import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";

import { Observable } from "rxjs/Observable";
import { ErrorObservable } from "rxjs/observable/ErrorObservable";
import "rxjs/add/observable/throw";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";

export interface AdminPostMetadata {
    id: number;
    slug: string;
    title: string;
    whenCreated: Date;
    whenPublished: Date;
    description: string;
    tags: string[];
}

export interface AdminPost {
    metadata: AdminPostMetadata;
    markdownContent: string;
}

export interface PostMetadata {
    title: string;
    description: string;
    id: number;
    slug: string;
    whenPublished: Date;
    tags: Array<string>;
}

export interface Post {
    metadata: PostMetadata;
    rawHtmlThenIGuess: string;
}

export interface MarkdownPreview {
    content: string;
}

export class NotFoundError {
}

export class UnknownError {
    public originalError: any;
    public errorMessage: string;

    constructor(error?: any) {
        if (error) {
            this.originalError = error;

            // Try to parse error message
            if (typeof error === "string") {
                this.errorMessage = error;
            }
            else if (error instanceof Response) {
                this.errorMessage = `[Http Status Code ${error.status}] ${error.statusText}`
            }
            else if (error instanceof Error) {
                this.errorMessage = `[Error ${error.name}] ${error.message}`
            }
            else {
                this.errorMessage = "Generic error message."
            }
        }
    }
}

const BackendEndpoint = "http://caticake.azurewebsites.net";
const handleFetchError = (error: any): ErrorObservable => {
    if (error instanceof Response && error.status === 404) {
        return Observable.throw(new NotFoundError());
    }

    const unknownError = new UnknownError(error);
    console.error(`${unknownError.errorMessage} %o`, error);
    return Observable.throw(unknownError);
};

@Injectable()
export class BackendApiService {
    constructor(private http: Http) { }

    public getRecentPostMetadata(count: number): Observable<PostMetadata[]> {
        return this.http.get(`${BackendEndpoint}/postmetadata?top=${count}`)
            .map<Response, PostMetadata[]>((response) => {
                const responseArray: PostMetadata[] = response.json();
                return responseArray.map((postMetadata) => this.parseServerPostMetadata(postMetadata));
            });
    }

    public getPost(slug: string): Observable<Post> {
        return this.http.get(`${BackendEndpoint}/post/${slug}`)
            .map<Response, Post>((response) => {
                const post: Post = response.json();
                post.metadata = this.parseServerPostMetadata(post.metadata);
                return post;
            })
            .catch(handleFetchError);
    }

    public getAdminPostMetadata(count = 1000): Observable<AdminPostMetadata[]> {
        return this.http.get(`${BackendEndpoint}/admin/post?top=${count}`)
            .map<Response, AdminPostMetadata[]>((response) => {
                const responseArray: AdminPostMetadata[] = response.json();
                return responseArray.map((adminPostMetadata) => this.parseServerAdminPostMetadata(adminPostMetadata));
            });
    }

    public getAdminPost(id: string): Observable<AdminPost> {
        return this.http.get(`${BackendEndpoint}/admin/post/${id}`)
            .map<Response, AdminPost>((response) => {
                const post: AdminPost = response.json();
                post.metadata = this.parseServerAdminPostMetadata(post.metadata);
                return post;
            })
            .catch(handleFetchError);
    }

    public setAdminPost(post: AdminPost): Observable<AdminPost> {
        return this.http.post(`${BackendEndpoint}/admin/post`, post)
            .catch(handleFetchError);
    }

    public getMarkdownPreview(markdown: string): Observable<MarkdownPreview> {
        return this.http.post(`${BackendEndpoint}/admin/previewmarkdown`, { markdown })
            .map<Response, MarkdownPreview>((response) => {
                return response.json();
            })
            .catch(handleFetchError);
    }

    /**
     * Ensures that the post metadata is in the format expected by the client
     * (i.e. Dates are proper JS Date objects and not strings, etc.)
     */
    private parseServerPostMetadata(postMetadata: PostMetadata): PostMetadata {
        postMetadata.whenPublished = new Date(postMetadata.whenPublished);
        return postMetadata;
    }

    private parseServerAdminPostMetadata(adminPostMetadata: AdminPostMetadata): AdminPostMetadata {
        adminPostMetadata.whenCreated = new Date(adminPostMetadata.whenCreated);
        adminPostMetadata.whenPublished = new Date(adminPostMetadata.whenPublished);
        return adminPostMetadata;
    }
}
