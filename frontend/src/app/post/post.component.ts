import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";

import { BackendApiService, PostMetadata, NotFoundError } from "../core/backend-api.service";

enum State {
    Loading,
    Normal
}

@Component({
    selector: "app-post",
    templateUrl: "./post.component.html",
    styleUrls: [ "./post.component.scss" ]
})
export class PostComponent implements OnInit {
    public StateEnum = State;
    public state = State.Loading;
    public postHtml: string;
    public tags: Array<string>;

    constructor(
        private backend: BackendApiService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit() {
        this.route.paramMap.subscribe((paramMap) => {
            this.state = State.Loading;

            const slug = paramMap.get("slug");
            this.backend.getPost(slug).subscribe(
                (post) => {
                    this.postHtml = post.rawHtmlThenIGuess;
                    this.tags = post.metadata.tags;
                    this.state = State.Normal;
                },
                (error) => {
                    if (error instanceof NotFoundError) {
                        this.router.navigateByUrl("notfound", { skipLocationChange: true });
                    }
                    else {
                        console.error(
                            `Unrecognized error:\n`
                            + JSON.stringify(error)
                        );
                    }
                }
            );
        });
    }
}
