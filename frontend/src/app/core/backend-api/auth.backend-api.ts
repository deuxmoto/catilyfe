import { Injectable } from "@angular/core";
import { HttpClient, HttpResponseBase, HttpErrorResponse } from "@angular/common/http";
import { Router } from "@angular/router";

import { Observable } from "rxjs/Observable";
import { ReplaySubject } from "rxjs/ReplaySubject";
import "rxjs/add/observable/throw";
import "rxjs/add/operator/do";

import * as Constants from "./constants";
import * as Errors from "./errors";

export type UserRole = "god-post";

export interface User {
    id: number;
    name: string;
    email: string;
    roles: UserRole[]
}

export import RedirectQueryParamName = Constants.RedirectQueryParamName;

@Injectable()
export class AuthBackendApi {
    private _loggedInUser = new ReplaySubject<User>(1);

    constructor(
        protected http: HttpClient,
        protected router: Router
    ) {
        this._refreshLoggedInUser();
    }

    public login(email: string, password: string): Observable<void> {
        const credentials = {
            email: email,
            password: password
        };
        return this.http.put<void>(`${Constants.Endpoint}/login`, credentials, { withCredentials: true })
            .catch((error) => {
                return Observable.throw(Errors.parseError(error));
            })
            .do((result) => {
                // Refresh logged in user if login was successful
                if (!Errors.isError(result)) {
                    this._refreshLoggedInUser();
                }
            });
    }

    public logout(): Observable<void> {
        return this.http.delete<void>(`${Constants.Endpoint}/login`, { withCredentials: true })
            .catch((error) => {
                return Observable.throw(Errors.parseError(error));
            })
            .do((result) => {
                // If logout was successful, set logged in user to null
                if (!Errors.isError(result)) {
                    this._loggedInUser.next(null);
                }
            });
    }

    public getLoggedInUser(): Observable<User> {
        return this._loggedInUser.asObservable();
    }

    private _refreshLoggedInUser(): void {
        this.http.get<User>(`${Constants.Endpoint}/admin/user/me`, { withCredentials: true })
            .subscribe(
                (user) => {
                    this._loggedInUser.next(user);
                },
                (error) => {
                    const parsedError = Errors.parseError(error);
                    if (parsedError instanceof Errors.UnauthorizedError) {
                        this._loggedInUser.next(null);
                    }
                    else {
                        console.error(parsedError);
                    }
                }
            );
    }
}
