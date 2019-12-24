import { Component, OnInit, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { MatDialog, Sort } from '@angular/material';
import { AppComponentBase } from '@shared/app-component-base';
import { CreateOrUpdateEstimateComponent } from './create-or-update-estimate/create-or-update-estimate.component';
import {
  EstimateListDto,
  EstimateServiceProxy,
  CreateEstimateDto
} from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
@Component({
  selector: 'app-estimates',
  templateUrl: './estimates.component.html',
  styleUrls: ['./estimates.component.css'],
  animations: [appModuleAnimation()]
})
export class EstimatesComponent extends AppComponentBase implements OnInit {
  estimates: EstimateListDto[] = [];
  firstName: string;
  lastName: string;

  // panigator
  public pageSize = 10;
  public pageNumber = 1;
  public totalPages = 1;
  public totalItems: number;
  keyword: string;
  skipCount = (this.pageNumber - 1) * this.pageSize;
  public isTableLoading = false;
  constructor(
    injector: Injector,
    private _dialog: MatDialog,
    private _estimateServiceProxy: EstimateServiceProxy
  ) {
    super(injector);
  }

  ngOnInit() {
    this.getAll();
  }
  getAll() {
    this.skipCount = (this.pageNumber - 1) * this.pageSize;
    this.isTableLoading = true;
    this._estimateServiceProxy
      .getAll(this.firstName, this.lastName, this.skipCount, this.pageSize)
      .subscribe(result => {
        this.estimates = result.items;
        this.totalItems = result.totalCount;
        this.totalPages =
          (result.totalCount - (result.totalCount % this.pageSize)) /
            this.pageSize +
          1;
        this.isTableLoading = false;
      });
  }
  createEstimate(): void {
    this.showCreateOrEditUserDialog();
  }
  // tslint:disable-next-line:no-unused-expression
  delete(estimate: EstimateListDto) {
    this.message.confirm(
      this.l('AreYouSureWantToDelete', estimate.firstname),
      this.l('AreYouSure'),
      isConfirmed => {
        if (isConfirmed) {
          this._estimateServiceProxy.delete(estimate.id).subscribe(result => {
            this.getAll();
            this.notify.info(this.l('DeleteSuccessfully'));
          });
        }
      }
    );
  }

  sortData(sort: Sort) {
    const data = this.estimates.slice();
    if (!sort.active || sort.direction === '') {
      this.estimates = data;
      return;
    }
    this.estimates = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'firstname': return compare(a.firstname, b.firstname, isAsc);
        case 'lastName': return compare(a.lastName, b.lastName, isAsc);
        case 'mobile': return compare(a.mobile, b.mobile, isAsc);
        case 'address': return compare(a.address, b.address, isAsc);
        case 'totalAmount': return compare(a.totalAmount, b.totalAmount, isAsc);
        default: return 0;
      }
    });
  }
  onChangedPanigation(event) {
    this.pageSize = event.pageSize;
    this.pageNumber = event.pageIndex + 1;
    this.getAll();
  }
  getDataPage(page: number): void {
    this.skipCount = (page - 1) * this.pageSize;
    this.pageNumber = page;
    this.getAll();
  }
  private showCreateOrEditUserDialog(id?: number): void {
    let createOrEditUserDialog;
    if (id === undefined || id <= 0) {
      createOrEditUserDialog = this._dialog.open(
        CreateOrUpdateEstimateComponent
      );
    } else {
      createOrEditUserDialog = this._dialog.open(
        CreateOrUpdateEstimateComponent,
        {
          data: id
        }
      );
    }

    createOrEditUserDialog.afterClosed().subscribe(result => {
      if (result) {
      }
    });
  }
}
function compare(a: number | string | moment.Moment, b: number | string | moment.Moment, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}

