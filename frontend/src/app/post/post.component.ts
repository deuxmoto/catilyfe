import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";

import { NotFoundError } from "../core/backend-api/errors";
import { AuthBackendApi, isUserAdmin } from "../core/backend-api/auth.backend-api";
import { Post, PostsBackendApi } from "../core/backend-api/posts.backend-api";

type State = "normal" | "loading";

@Component({
    selector: "app-post",
    templateUrl: "./post.component.html",
    styleUrls: [ "./post.component.scss" ]
})
export class PostComponent implements OnInit {
    public state: State = "loading";
    public post: Post;

    public isUserAdmin: boolean;

    constructor(
        private authBackend: AuthBackendApi,
        private postsBackend: PostsBackendApi,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    public get postHtml(): string {
        return this.post && this.post.rawHtmlThenIGuess;
    }

    public get editPostUrl(): string {
        const postId = this.post && this.post.metadata.id;
        return postId && `/admin/editpost/${postId}`;
    }

    public ngOnInit() {
        this.route.paramMap.subscribe((paramMap) => {
            this.state = "loading";

            const slug = paramMap.get("slug");
            this.postsBackend.getPost(slug).subscribe(
                (post) => {
                    this.post = post;
                    this.state = "normal";
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

        this.authBackend.getLoggedInUser().subscribe((user) => {
            this.isUserAdmin = isUserAdmin(user);
        });
    }
}
