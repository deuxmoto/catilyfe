import { Location } from "@angular/common";
import { Component, OnInit, OnDestroy, ViewChild } from "@angular/core";
import { FormControl, FormGroup, Validators, ControlValueAccessor } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { DataSource } from "@angular/cdk/collections";

import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/fromEvent";
import "rxjs/add/operator/map";

import { MarkdownPreviewComponent } from "./markdown-preview/markdown-preview.component";
import {
    BackendApiService, AdminPost, AdminPostMetadata,
    NotFoundError, OtherError
} from "../../core/backend-api.service";

enum State {
    Normal,
    Loading,
    Saving
}

interface DisplayTab {
    text: string;
    value: Tab;
}
enum Tab {
    Metadata,
    Markdown,
    MarkdownPreview
}

@Component({
    selector: "edit-post",
    templateUrl: "./edit-post.component.html",
    styleUrls: [ "./edit-post.component.scss" ]
})
export class EditPostComponent implements OnInit {
    public metadataForm: FormGroup;

    // The following fields are either not included in the above metadataForm
    // or are duplicated, because they're being used in parts of the UI that can't
    // read from or work with a FormGroup easily:
    public content = "";
    public tags: string[] = [];
    public petersStupidRevisionIdShit: number;

    public StateEnum = State;
    public state = State.Loading;
    public savingText: string;
    public newPost = false;
    public lastError: string;

    public TabEnum = Tab;
    public tabs: DisplayTab[] = [
        {
            text: "Metadata",
            value: Tab.Metadata
        },
        {
            text: "Markdown",
            value: Tab.Markdown
        },
        {
            text: "Markdown Preview",
            value: Tab.MarkdownPreview
        }
    ];
    public currentTab = Tab.Metadata;

    constructor(
        private backend: BackendApiService,
        private route: ActivatedRoute,
        private router: Router,
        private location: Location
    ) { }

    public ngOnInit(): void {
        // Create the form controls
        this.metadataForm = new FormGroup({
            id: new FormControl({ value: "", disabled: true }),
            title: new FormControl(""),
            description: new FormControl(""),
            slug: new FormControl(""),
            whenCreated: new FormControl({ value: "", disabled: true }),
            whenPublished: new FormControl(new Date()),
            isPublished: new FormControl(false),
            isReserved: new FormControl(false),
            newTag: new FormControl("")
        });

        const id = this.route.snapshot.paramMap.get("id");
        if (!id) {
            // New post
            this.state = State.Normal;
            this.newPost = true;
        }
        else {
            // Existing post
            this.backend.getAdminPost(id).subscribe(
                (post) => {
                    const metadata = post.metadata;
                    this.metadataForm.reset({
                        id: metadata.id,
                        title: metadata.title,
                        description: metadata.description,
                        slug: metadata.slug,
                        whenCreated: metadata.whenCreated,
                        whenPublished: metadata.whenPublished,
                        isPublished: metadata.isPublished,
                        isReserved: metadata.isReserved
                    });
                    this.tags = metadata.tags;
                    this.content = post.markdownContent;
                    this.petersStupidRevisionIdShit = metadata.revision;

                    this.state = State.Normal;
                },
                (error) => {
                    this.handleNetworkError(error);
                }
            );
            this.newPost = false;
        }
    }

    public closeEditPost(): void {
        this.location.back();
    }

    public savePost(): void {
        const formMetadata = this.metadataForm;
        const adminPost: AdminPost = {
            metadata: {
                title: formMetadata.get("title").value,
                description: formMetadata.get("description").value,
                slug: formMetadata.get("slug").value,
                whenPublished: formMetadata.get("whenPublished").value,
                isReserved: formMetadata.get("isReserved").value,
                isPublished: formMetadata.get("isPublished").value,
                tags: this.tags
            },
            markdownContent: this.content
        };

        // If we're editing an existing post, make sure the post id is set;
        // this tells the backend that this is an update
        if (!this.newPost) {
            adminPost.metadata.id = formMetadata.get("id").value;
            adminPost.metadata.revision = this.petersStupidRevisionIdShit;
        }

        this.state = State.Saving;
        this.savingText = "Saving...";
        this.lastError = "";
        this.backend.setAdminPost(adminPost).subscribe(
            () => {
                this.savingText = "Saved! Yeeee";
                this.state = State.Normal;
            },
            (error) => {
                this.savingText = "Save failed :(";
                this.handleNetworkError(error);
                this.state = State.Normal;
            }
        );
    }

    public setCurrentTab(tab: Tab): void {
        this.currentTab = tab;
    }

    public addTag(keypress?: KeyboardEvent): void {
        const newTagTextBox = this.metadataForm.get("newTag");
        const newTagValue = newTagTextBox.value;

        // Only add tag for Enter key
        if (keypress && keypress.keyCode !== 13 || !newTagValue) {
            return;
        }

        this.tags.push(newTagValue);
        newTagTextBox.reset("");
    }

    public closeErrorMessage(): void {
        this.lastError = "";
    }

    private handleNetworkError(error: any): void {
        if (error instanceof NotFoundError) {
            this.router.navigateByUrl("notfound", { skipLocationChange: true });
        }
        else if (error instanceof OtherError) {
            this.lastError = error.errorMessage;
        }
    }
}
