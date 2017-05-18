import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";

export interface PostMetadata {
    title: string;
    description: string;
    id: number;
    slug: string;
    whenPublished: Date;
}

const BackendEndpoint = "http://caticake.azurewebsites.net";

@Injectable()
export class BackendApiService {

    constructor(private http: Http) { }

    public getRecentPostMetadata(count: number): Observable<PostMetadata[]> {
        return this.http.get(`${BackendEndpoint}/postmetadata?$top=${count}`)
            .map<Response, PostMetadata[]>((response) => {
                const responseArray: PostMetadata[] = response.json();
                return responseArray.map((postMetadata) => {
                    // Ensure that whenPublished is a date object
                    postMetadata.whenPublished = new Date(postMetadata.whenPublished);
                    return postMetadata;
                });
            });
    }
}
