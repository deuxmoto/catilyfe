import { HttpErrorResponse } from "@angular/common/http";

export abstract class BaseError {
    public originalError: any;
    public errorMessage: string;

    constructor(error?: any) {
        this.originalError = error;
    }

    public toString(): string {
        return this.errorMessage;
    }
}

export class NotFoundError extends BaseError {
    public errorMessage = "Not found";
}

export class UnauthorizedError extends BaseError {
    public errorMessage = "Unauthorized";
}

export class OtherError extends BaseError {
    public originalError: any;
    public errorMessage: string;

    constructor(error?: any) {
        super(error);
        if (error) {
            // Try to parse error message
            if (typeof error === "string") {
                this.errorMessage = error;
            }
            else if (error instanceof HttpErrorResponse) {
                const innerError = error.error;
                if (innerError.message) {
                    // Caticake error
                    this.errorMessage = `[Error ${innerError.message}] ${JSON.stringify(innerError.details)}`;
                }
                else {
                    this.errorMessage = `[Http Status Code ${error.status}] ${error.statusText}`
                }
            }
            else if (error instanceof Error) {
                this.errorMessage = `[Error ${error.name}] ${error.message}`
            }
            else {
                this.errorMessage = "Unknown error."
            }
        }
    }
}

export function isError(obj: any): boolean {
    return obj instanceof BaseError;
}

export function parseError(error: any): BaseError {
    if (error instanceof HttpErrorResponse) {
        switch (error.status) {
            case 401:
                return new UnauthorizedError(error);
            case 404:
                return new NotFoundError(error);
        }
    }

    return new OtherError(error);
}
