import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";

import { BackendApiService, PostMetadata, PostNotFoundError } from "../core/backend-api.service";

enum State {
    Loading,
    InvalidPost,
    Normal
}

@Component({
    selector: "app-post",
    templateUrl: "./post.component.html",
    styleUrls: [ "./post.component.scss" ]
})
export class PostComponent implements OnInit {
    private StateEnum = State;
    private state = State.Loading;
    private postHtml: string;

    constructor(
        private backend: BackendApiService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit() {
        const slug = this.route.snapshot.paramMap.get("slug");
        this.backend.getPost(slug).subscribe(
            (post) => {
                this.postHtml = post.rawHtmlThenIGuess;
                this.state = State.Normal;
            },
            (error) => {
                this.state = State.InvalidPost;
                if (error instanceof PostNotFoundError) {
                    console.error("invalid post");
                }
                return null;
            }
        );
    }
}
