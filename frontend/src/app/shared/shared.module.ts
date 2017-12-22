import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";

import { FooterComponent } from "./footer/footer.component";
import { PostPreviewComponent } from "./post-preview/post-preview.component";
import { TitleBarComponent } from "./title-bar/title-bar.component";
import { TitleBarService } from "./title-bar/title-bar.service";
import { TitleBarMenuComponent } from "./title-bar/title-bar-menu/title-bar-menu.component";

@NgModule({
    imports: [
        CommonModule,
        RouterModule
    ],
    declarations: [
        FooterComponent,
        PostPreviewComponent,
        TitleBarComponent,
        TitleBarMenuComponent
    ],
    exports: [
        FooterComponent,
        PostPreviewComponent,
        TitleBarComponent,
        CommonModule,
        FormsModule,
        ReactiveFormsModule
    ],
    providers: [
        TitleBarService
    ]
})
export class SharedModule { }
