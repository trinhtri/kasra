import { Component, OnInit, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { MatDialog } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { CreateOrUpdateEstimateComponent } from './create-or-update-estimate/create-or-update-estimate.component';

@Component({
  selector: 'app-estimates',
  templateUrl: './estimates.component.html',
  styleUrls: ['./estimates.component.css'],
  animations: [appModuleAnimation()],
})
export class EstimatesComponent extends AppComponentBase implements OnInit  {

  constructor( injector: Injector,
              private _dialog: MatDialog) {
                super(injector);
              }

  ngOnInit() {
  }
  createEstimate(): void {
    this.showCreateOrEditUserDialog();
}
private showCreateOrEditUserDialog(id?: number): void {
  let createOrEditUserDialog;
  if (id === undefined || id <= 0) {
      createOrEditUserDialog = this._dialog.open(CreateOrUpdateEstimateComponent);
  } else {
      createOrEditUserDialog = this._dialog.open(CreateOrUpdateEstimateComponent, {
          data: id
      });
  }

  createOrEditUserDialog.afterClosed().subscribe(result => {
      if (result) {
      }
  });
}
}
