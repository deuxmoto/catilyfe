import { NgModule } from "@angular/core";
import { MatButtonModule, MatCardModule, MatInputModule, MatTableModule } from "@angular/material";
import { Route, RouterModule } from "@angular/router";

import { AdminComponent } from "./admin.component";
import { EditPostComponent } from "./edit-post/edit-post.component";
import { MarkdownPreviewComponent } from "./edit-post/markdown-preview/markdown-preview.component";

import { SharedModule } from "../shared/shared.module";

const adminRoutes: Route[] = [
    {
        path: "admin",
        component: AdminComponent
    },
    {
        path: "admin/editpost/:id",
        component: EditPostComponent
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(adminRoutes),
        MatButtonModule,
        MatCardModule,
        MatInputModule,
        MatTableModule,

        SharedModule
    ],
    declarations: [
        AdminComponent,
        EditPostComponent,
        MarkdownPreviewComponent
    ]
})
export class AdminModule { }
