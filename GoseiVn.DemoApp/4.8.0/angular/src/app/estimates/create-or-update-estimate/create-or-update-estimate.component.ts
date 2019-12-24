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
}
