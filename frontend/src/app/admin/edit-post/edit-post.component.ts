import { Component, OnInit, OnDestroy } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { DataSource } from "@angular/cdk/collections";
import { MdSort } from "@angular/material";

import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/fromEvent";
import "rxjs/add/operator/map";

import {
    BackendApiService, AdminPost, AdminPostMetadata,
    NotFoundError, UnknownError
} from "../../core/backend-api.service";

enum State {
    Normal,
    Loading,
    Saving,
    Error
}

@Component({
    selector: "edit-post",
    templateUrl: "./edit-post.component.html",
    styleUrls: [ "./edit-post.component.scss" ]
})
export class EditPostComponent implements OnInit {
    public id: number;
    public title: string;
    public description: string;
    public slug: string;
    public tags: string[];
    public whenCreated: Date;
    public whenPublished: Date;
    public content: string;

    public state = State.Loading;
    public StateEnum = State;
    public lastErrorMessage: string;
    public savingText: string;

    constructor(
        private backend: BackendApiService,
        private route: ActivatedRoute,
        private router: Router,
    ) { }

    public ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get("id");
        if (!id) {
            console.log("Yeah you can try creating a post, but it ain't gonna work");
            return;
        }

        this.backend.getAdminPost(id).subscribe(
            (post) => {
                const metadata = post.metadata;
                this.id = metadata.id;
                this.title = metadata.title;
                this.description = metadata.description;
                this.slug = metadata.slug;
                this.tags = metadata.tags;
                this.whenCreated = metadata.whenCreated;
                this.whenPublished = metadata.whenPublished;
                this.content = post.markdownContent;

                this.state = State.Normal;
            },
            (error) => {
                this.handleNetworkError(error);
            }
        );
    }

    public closeEditPost(): void {
        this.router.navigate([ "../../" ], { relativeTo: this.route });
    }

    public savePost(): void {
        const adminPost: AdminPost = {
            metadata: {
                id: this.id,
                title: this.title,
                description: this.description,
                slug: this.slug,
                tags: this.tags,
                whenCreated: this.whenCreated,
                whenPublished: this.whenPublished
            },
            markdownContent: this.content
        };

        this.state = State.Saving;
        this.savingText = "Saving...";
        this.backend.setAdminPost(adminPost).subscribe(
            () => {
                this.savingText = "Saved! Yeeee";
            },
            (error) => {
                this.handleNetworkError(error);
            },
            () => {
                this.state = State.Normal;
            }
        );
    }

    private handleNetworkError(error: any): void {
        if (error instanceof NotFoundError) {
            this.router.navigateByUrl("notfound", { skipLocationChange: true });
        }
        else if (error instanceof UnknownError) {
            this.lastErrorMessage = error.errorMessage;
        }

        this.state = State.Error;
    }
}
