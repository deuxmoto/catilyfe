import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { Route, RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
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
        path: "posts/:slug",
        component: PostComponent
    }
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    PostComponent
  ],
  imports: [
    RouterModule.forRoot(routes),
    CoreModule.forRoot(),
    BrowserModule,
    HttpModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
