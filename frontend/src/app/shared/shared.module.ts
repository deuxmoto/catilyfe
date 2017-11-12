import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";

import { FooterComponent } from "./footer/footer.component";
import { TitleBarComponent } from "./title-bar/title-bar.component";

@NgModule({
    imports: [ CommonModule, RouterModule ],
    declarations: [ FooterComponent, TitleBarComponent ],
    exports: [ FooterComponent, TitleBarComponent, CommonModule, FormsModule, ReactiveFormsModule ]
})
export class SharedModule { }
