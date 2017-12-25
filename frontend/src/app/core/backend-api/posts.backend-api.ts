import { Injectable } from "@angular/core";
import { HttpClient, HttpResponseBase, HttpErrorResponse } from "@angular/common/http";

import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/throw";

import * as Constants from "./constants";
import * as Errors from "./errors";

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

const parseDateObjects = (postMetadata: PostMetadata): void => {
    postMetadata.whenPublished = new Date(postMetadata.whenPublished);
};

@Injectable()
export class PostsBackendApi {
    constructor (
        private http: HttpClient
    ) {}

    public getPostMetadata(top: number): Observable<PostMetadata[]> {
        return this.http.get<PostMetadata[]>(`${Constants.Endpoint}/postmetadata?top=${top}`)
            .map((postMetadata) => {
                // Convert date strings to actual dates
                postMetadata.forEach((metadata) => {
                    parseDateObjects(metadata);
                });

                return postMetadata;
            });
    }

    public getPosts(top = 5): Observable<Post[]> {
        return this.http.get<Post[]>(`${Constants.Endpoint}/post?top=${top}`)
            .map((posts) => {
                posts.forEach((post) => {
                    parseDateObjects(post.metadata);
                });

                return posts;
            });
    }

    public getPost(slug: string): Observable<Post> {
        return this.http.get<Post>(`${Constants.Endpoint}/post/${slug}`)
            .map((post) => {
                parseDateObjects(post.metadata);
                return post;
            })
            .catch((error) => {
                return Errors.parseAndThrowError(error);
            });
    }
}
