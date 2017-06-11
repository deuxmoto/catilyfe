import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { Route, RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { CoreModule } from "./core/core.module";
import { HomeComponent } from "./home/home.component";
import { PostComponent } from "./post/post.component";

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
    FormsModule,
    HttpModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
