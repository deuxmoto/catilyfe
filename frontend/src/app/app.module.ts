import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from "@angular/core";
import { HttpModule } from "@angular/http";
import { MatButtonModule, MatCardModule, MatInputModule } from "@angular/material";
import { Route, RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { NotFoundComponent } from "./404/404.component";
import { AdminModule } from "./admin/admin.module";
import { CoreModule } from "./core/core.module";
import { HomeComponent } from "./home/home.component";
import { LoginComponent } from "./login/login.component";
import { PostComponent } from "./post/post.component";
import { TestPageComponent } from "./testpage/testpage.component";
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
        path: "testpage",
        component: TestPageComponent
    },
    {
        path: "login",
        component: LoginComponent
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
        LoginComponent,
        NotFoundComponent,
        PostComponent,
        TestPageComponent
    ],
    imports: [
        RouterModule.forRoot(routes),
        CoreModule.forRoot(),

        BrowserModule,
        BrowserAnimationsModule,
        HttpModule,
        MatButtonModule,
        MatCardModule,
        MatInputModule,

        AdminModule,
        SharedModule
    ],
    providers: [ ],
    bootstrap: [ AppComponent ]
})
export class AppModule { }
