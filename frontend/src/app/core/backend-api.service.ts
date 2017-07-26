import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";

import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/throw";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";

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

export class PostNotFoundError {
}

export class UnknownError {
}

const BackendEndpoint = "http://caticake.azurewebsites.net";

@Injectable()
export class BackendApiService {

    constructor(private http: Http) { }

    public getRecentPostMetadata(count: number): Observable<PostMetadata[]> {
        return this.http.get(`${BackendEndpoint}/postmetadata?$top=${count}`)
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
            .catch((error) => {
                if (error instanceof Response && error.status === 404) {
                    return Observable.throw(new PostNotFoundError());
                } else {
                    console.error(error);
                    return Observable.throw(new UnknownError());
                }
            });
    }

    /**
     * Ensures that the post metadata is in the format expected by the client
     * (i.e. Dates are proper JS Date objects and not strings, etc.)
     */
    private parseServerPostMetadata(postMetadata: PostMetadata): PostMetadata {
        postMetadata.whenPublished = new Date(postMetadata.whenPublished);
        return postMetadata;
    }
}
