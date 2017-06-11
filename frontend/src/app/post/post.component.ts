import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";

import { BackendApiService, PostMetadata, PostNotFoundError } from "../core/backend-api.service";

@Component({
    selector: "app-post",
    templateUrl: "./post.component.html",
    styleUrls: [ "./post.component.scss" ]
})
export class PostComponent implements OnInit {
    private postHtml: string;
    private validPost = true;

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
                this.validPost = true;
            },
            (error) => {
                if (error instanceof PostNotFoundError) {
                    this.validPost = false;
                    console.error("invalid post");
                }
                return null;
            }
        );
    }
}
