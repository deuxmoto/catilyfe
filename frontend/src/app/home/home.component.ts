import { Component, OnInit } from "@angular/core";

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

    constructor(private backend: BackendApiService) { }

    ngOnInit() {
        this.recentPosts = this.backend.getRecentPostMetadata(10);
    }
}
