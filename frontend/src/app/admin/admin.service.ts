import { Injectable } from "@angular/core";

import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";

@Injectable()
export class AdminService {

    private refreshAdminViewEmitter = new Subject<void>();
    private bodyOverflowEmitter = new Subject<boolean>();

    public hideBodyOverflow(hidden: boolean): void {
        this.bodyOverflowEmitter.next(hidden);
    }

    public refreshAdminView(): void {
        this.refreshAdminViewEmitter.next();
    }

    public observableBodyOverflow(): Observable<boolean> {
        return this.bodyOverflowEmitter.asObservable();
    }

    public observableRefreshAdminViewEmitter(): Observable<void> {
        return this.refreshAdminViewEmitter.asObservable();
    }
}
