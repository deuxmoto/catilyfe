import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";

import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";

import { AuthBackendApi, RedirectQueryParamName} from "../core/backend-api/auth.backend-api";

@Component({
    selector: "app-login",
    templateUrl: "./login.component.html",
    styleUrls: [ "./login.component.scss" ]
})
export class LoginComponent implements OnInit {
    public loginForm: FormGroup;
    private redirectUrl: string;

    constructor(
        private authBackendApi: AuthBackendApi,
        private route: ActivatedRoute,
        private router: Router,
        private fb: FormBuilder
    ) { }

    public ngOnInit() {
        this.loginForm = this.fb.group({
            email: ["", Validators.required],
            password: ["", Validators.required]
        });

        this.route.queryParams.subscribe((queryParams) => {
            this.redirectUrl = queryParams[RedirectQueryParamName];
        });
    }

    public onSubmit(): void {
        let email = this.loginForm.get("email").value;
        let password = this.loginForm.get("password").value;
        this.authBackendApi.login(email, password).subscribe(
            () => {
                let redirectUrl = this.redirectUrl ? this.redirectUrl : "";
                this.router.navigateByUrl(redirectUrl);
            },
            (error) => {
                console.error(error);
            }
        );
    }
}
