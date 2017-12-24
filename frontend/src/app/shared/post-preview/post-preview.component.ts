import { Component, Input, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";

import { PostsBackendApi, PostMetadata } from "../../core/backend-api/posts.backend-api";

@Component({
    selector: "post-preview",
    templateUrl: "./post-preview.component.html",
    styleUrls: [ "./post-preview.component.scss" ]
})
export class PostPreviewComponent implements OnInit {
    @Input()
    public set previewCount(count: number) {
        this.multiplePostMetadata = this.postsBackendApi.getPostMetadata(count);
    }

    public multiplePostMetadata: Observable<PostMetadata[]>;

    constructor(
        private postsBackendApi: PostsBackendApi,
        private router: Router
    ) { }

    public ngOnInit(): void {
    }

    public getPostLink(postMetadata: PostMetadata): string {
        return "/posts/" + postMetadata.slug;
    }

    public getPostImageUrl(postMetadata: PostMetadata): string {
        return "url(../../assets/placeholder.svg)";
    }
}
