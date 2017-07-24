import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { Route, RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { NotFoundComponent } from "./404/404.component";
import { CoreModule } from "./core/core.module";
import { HomeComponent } from "./home/home.component";
import { PostComponent } from "./post/post.component";
import { SharedModule } from "./shared/shared.module";

const routes: Route[] = [
    {
        path: "",
        component: HomeComponent
    },
    {
        path: "home",
        redirectTo: ""
    },
    {
        path: "posts/:slug",
        component: PostComponent
    },
    {
        path: "notfound",
        component: NotFoundComponent
    },
    {
        path: "**",
        component: NotFoundComponent
    }
];

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        PostComponent,
        NotFoundComponent
    ],
    imports: [
        RouterModule.forRoot(routes),
        CoreModule.forRoot(),
        BrowserModule,
        BrowserAnimationsModule,
        HttpModule,
        SharedModule
    ],
    providers: [],
    bootstrap: [ AppComponent ]
})
export class AppModule { }
