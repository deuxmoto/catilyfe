import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";

import { BackendApiService, PostMetadata } from "../core/backend-api.service";

@Component({
    selector: "app-home",
    templateUrl: "./home.component.html",
    styleUrls: [ "./home.component.scss" ]
})
export class HomeComponent implements OnInit {
    private recentPosts: Observable<PostMetadata[]>;

    constructor(
        private backend: BackendApiService,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.recentPosts = this.backend.getRecentPostMetadata(10);
    }

    navigateToPost(postSlug: string): void {
        this.router.navigate(["posts", postSlug]);
    }
}
