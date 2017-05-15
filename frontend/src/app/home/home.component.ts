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
    private recentPosts: Observable<string[]>;

    constructor(private backend: BackendApiService) { }

    ngOnInit() {
        this.recentPosts = this.backend.getPostMetadata()
            .map((postMetadata): string[] => {
                return postMetadata.map((pm) => JSON.stringify(pm));
            });
    }

}
