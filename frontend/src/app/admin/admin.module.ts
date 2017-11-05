import { NgModule } from "@angular/core";
import { MdButtonModule, MdCardModule, MdInputModule, MdTableModule } from "@angular/material";
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
        MdButtonModule,
        MdCardModule,
        MdInputModule,
        MdTableModule,

        SharedModule
    ],
    declarations: [
        AdminComponent,
        EditPostComponent,
        MarkdownPreviewComponent
    ]
})
export class AdminModule { }
