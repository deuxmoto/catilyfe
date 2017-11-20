import { NgModule } from "@angular/core";
import { MatButtonModule, MatCardModule, MatDatepickerModule, MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule, MatOptionModule, MatSelectModule, MatTableModule } from "@angular/material";
import { Route, RouterModule } from "@angular/router";

import { AdminComponent } from "./admin.component";
import { EditPostComponent } from "./edit-post/edit-post.component";
import { DateTimePickerComponent } from "./edit-post/datetime-picker/datetime-picker.component";
import { PadTimePipe } from "./edit-post/datetime-picker/pad-time.pipe";
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
        MatDatepickerModule,
        MatNativeDateModule,
        MatFormFieldModule,
        MatInputModule,
        MatOptionModule,
        MatSelectModule,
        MatTableModule,

        SharedModule
    ],
    declarations: [
        AdminComponent,
        DateTimePickerComponent,
        EditPostComponent,
        MarkdownPreviewComponent,
        PadTimePipe
    ]
})
export class AdminModule { }
