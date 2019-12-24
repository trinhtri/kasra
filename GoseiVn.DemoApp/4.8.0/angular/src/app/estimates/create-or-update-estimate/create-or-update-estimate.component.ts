import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-create-or-update-estimate',
  templateUrl: './create-or-update-estimate.component.html',
  styleUrls: ['./create-or-update-estimate.component.css']
})
export class CreateOrUpdateEstimateComponent  extends AppComponentBase implements OnInit {
  saving = false;
  color = '#5a24ea';
  accept = '*';
  files: File[] = [];
  progress: number;
  lastFileAt: Date;

  dragFiles: any;
  validComboDrag: any;
  lastInvalids: any;
  fileDropDisabled: any;
  maxSize: any;
  baseDropValid: any;

  constructor(injector: Injector,
    private _dialogRef: MatDialogRef<CreateOrUpdateEstimateComponent>
    ) {
    super(injector);
  }

  ngOnInit() {
  }
  close(result: any): void {
    this._dialogRef.close(result);
  }

  colorChanged(e: Event) {
    console.log(e);
  }

  getDate() {
    return new Date();
  }

  save() {
    console.log(this.files);
  }
}
