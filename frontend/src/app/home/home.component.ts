import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/observable/fromEvent";
import "rxjs/add/operator/throttleTime";
import "rxjs/add/operator/delay";

import { BackendApiService, PostMetadata } from "../core/backend-api.service";

const scrollDebounceIntervalMs = 200;

@Component({
    selector: "app-home",
    templateUrl: "./home.component.html",
    styleUrls: [ "./home.component.scss" ]
})
export class HomeComponent implements OnInit {
    public recentPosts: Observable<PostMetadata[]>;
    public titleBarTheme: string = "transparent-light";

    constructor(
        private backend: BackendApiService,
        private router: Router
    ) { }

    public ngOnInit(): void {
        this.recentPosts = this.backend.getRecentPostMetadata(10);

        // Listen to scroll events
        Observable.fromEvent(window, "scroll")
            .delay(scrollDebounceIntervalMs / 2)
            .throttleTime(scrollDebounceIntervalMs / 2)
            .subscribe(this.onScroll.bind(this));
    }

    public navigateToPost(postSlug: string): void {
        this.router.navigate(["posts", postSlug]);
    }

    public navigateToTag(tag: string): void {
        this.router.navigate(["/"]);
    }

    public onScroll(): void {
        this.titleBarTheme = window.scrollY < 5 ? "transparent-light" : "light";
    }
}
