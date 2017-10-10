import { NgModule } from "@angular/core";
import { MdButtonModule, MdCardModule, MdInputModule, MdTableModule } from "@angular/material";
import { Route, RouterModule } from "@angular/router";

import { AdminComponent } from "./admin.component";
import { AdminService } from "./admin.service";
import { EditPostComponent } from "./edit-post/edit-post.component";

import { SharedModule } from "../shared/shared.module";

const adminRoutes: Route[] = [
    {
        path: "admin",
        component: AdminComponent,
        children: [
            {
                path: "editpost/:id",
                component: EditPostComponent
            }
        ]
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
        EditPostComponent
    ],
    providers: [ AdminService ]
})
export class AdminModule { }
