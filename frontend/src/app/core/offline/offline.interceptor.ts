import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpResponse,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/add/observable/of";

import * as OfflineRoutes from "./offline-routes";
import { BackendHostName } from "../backend-api.service";

const relativeUrlRegex = new RegExp(`^.*${BackendHostName.replace(/\./g, "\\.")}\/((?:[^\?#])*)`);

@Injectable()
export class OfflineInterceptor implements HttpInterceptor {
    constructor() {
        console.warn("Running catilyfe in offline mode.");
    }

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const requestUrl = request.url;
        const parsedUrl = request.url.match(relativeUrlRegex);
        const relativeUrl = parsedUrl.length > 1 ? parsedUrl[1] : "";

        // Remove all non-word characters
        const convertedUrlName = relativeUrl.replace(/\W/g, "");

        const responseBody = OfflineRoutes[convertedUrlName];
        if (responseBody) {
            console.info(`Intercepted route ${convertedUrlName}`);
            const response = new HttpResponse({
                body: responseBody
            });
            return Observable.of(response);
        }

        throw new Error(
            `Could not find an offline route for '${convertedUrlName}'. ` +
            `Configured offline routes are: ${Object.keys(OfflineRoutes).join(",")}`
        );
    }
}
