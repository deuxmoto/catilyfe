import { Component, HostBinding, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { DataSource } from "@angular/cdk/collections";
import { MdSort } from "@angular/material";

import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";
import "rxjs/add/operator/map";

import { BackendApiService, AdminPostMetadata } from "../core/backend-api.service";

@Component({
    selector: "app-admin",
    templateUrl: "./admin.component.html",
    styleUrls: [ "./admin.component.scss" ]
})
export class AdminComponent implements OnInit {
    public dataSource: AdminPostMetadataDatasource;
    public displayedColumns = [
        "postId",
        "postSlug",
        "postTitle",
        "postWhenCreated"
    ];

    constructor(
        private backend: BackendApiService,
        private route: ActivatedRoute,
        private router: Router,
    ) { }

    public ngOnInit(): void {
        this.dataSource = new AdminPostMetadataDatasource(this.backend);
    }

    public editPost(id: string): void {
        this.router.navigate(["editpost", id], { relativeTo: this.route });
    }
}

export class AdminPostMetadataDatasource extends DataSource<AdminPostMetadata> {
    private adminPostMetadata = new Subject<AdminPostMetadata[]>();

    constructor(
        private backend: BackendApiService
    ) {
        super();
    }

    public connect(): Observable<AdminPostMetadata[]> {
        this.refresh();
        return this.adminPostMetadata.asObservable();
    }

    public disconnect(): void { }

    public refresh(): void {
        this.backend.getAdminPostMetadata()
            .subscribe((adminPostMetadata) => {
                this.adminPostMetadata.next(adminPostMetadata);
            });
    }
}
