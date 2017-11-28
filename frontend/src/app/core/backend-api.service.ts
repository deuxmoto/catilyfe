import { Injectable } from "@angular/core";
import { ActivatedRoute, Router, Params as QueryParams } from "@angular/router";
import { HttpClient, HttpHeaders, HttpErrorResponse } from "@angular/common/http";

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
    isReserved: boolean;
    isPublished: boolean;
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

export class UnauthorizedError {
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

export const RedirectQueryParamName = "redirectUrl";

export const BackendHostName = "caticake.azurewebsites.net";
export const BackendProtocol = "https";

const BackendEndpoint = `${BackendProtocol}://${BackendHostName}`;

const convertDateStringsToObjects = <T>(obj: T): T => {
    let anyObj = <any>obj;
    if (anyObj.whenCreated) {
        anyObj.whenCreated = new Date(anyObj.whenCreated);
    }
    if (anyObj.whenPublished) {
        anyObj.whenPublished = new Date(anyObj.whenPublished);
    }
    return anyObj;
};

@Injectable()
export class BackendApiService {
    constructor(
        private http: HttpClient,
        private router: Router
    ) { }

    public getRecentPostMetadata(count: number): Observable<PostMetadata[]> {
        return this.http.get<PostMetadata[]>(`${BackendEndpoint}/postmetadata?top=${count}`)
            .map((postMetadata) => {
                // Convert date strings to actual dates
                postMetadata.forEach((metadata) => {
                    convertDateStringsToObjects(metadata);
                });

                return postMetadata;
            });
    }

    public getPost(slug: string): Observable<Post> {
        return this.http.get<Post>(`${BackendEndpoint}/post/${slug}`)
            .map((post) => {
                convertDateStringsToObjects(post.metadata);
                return post;
            })
            .catch((error) => {
                return this._handleFetchError(error);
            });
    }

    public getAdminPostMetadata(count = 1000): Observable<AdminPostMetadata[]> {
        return this.http.get<AdminPostMetadata[]>(`${BackendEndpoint}/admin/post?top=${count}`, {
            withCredentials: true
        })
            .map((postMetadata) => {
                postMetadata.forEach((metadata) => {
                    convertDateStringsToObjects(metadata);
                });
                return postMetadata;
            })
            .catch((error) => {
                return this._handleFetchError(error);
            });
    }

    public getAdminPost(id: string): Observable<AdminPost> {
        return this.http.get<AdminPost>(`${BackendEndpoint}/admin/post/${id}`, {
            withCredentials: true
        })
            .map((adminPost) => {
                convertDateStringsToObjects(adminPost.metadata);
                return adminPost;
            })
            .catch((error) => {
                return this._handleFetchError(error);
            });
    }

    public setAdminPost(post: AdminPost): Observable<AdminPost> {
        return this.http.post(`${BackendEndpoint}/admin/post`, post, {
            withCredentials: true
        })
            .catch((error) => {
                return this._handleFetchError(error);
            });
    }

    public getMarkdownPreview(markdown: string): Observable<MarkdownPreview> {
        return this.http.post<MarkdownPreview>(`${BackendEndpoint}/admin/previewmarkdown`, { markdown }, {
            withCredentials: true
        })
            .catch((error) => {
                return this._handleFetchError(error);
            });
    }

    public loginUser(email: string, password: string): Observable<void> {
        const credentials = {
            email: email,
            password: password
        };
        return this.http.put<void>(`${BackendEndpoint}/login`, credentials, { withCredentials: true });
    }

    private _handleFetchError(error: any): ErrorObservable {
        if (error instanceof HttpErrorResponse) {
            switch (error.status) {
                case 404:
                    return Observable.throw(new NotFoundError());
                case 401:
                    const redirectUrl = this.router.url;
                    this.router.navigate(["/login"], {
                        queryParams: {
                            [RedirectQueryParamName]: redirectUrl
                        }
                    });
                    return Observable.throw(new UnauthorizedError());
            }
        }

        const unknownError = new UnknownError(error);
        console.error(`${unknownError.errorMessage} %o`, error);
        return Observable.throw(unknownError);
    }
}
