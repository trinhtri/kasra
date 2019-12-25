import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef } from '@angular/material';
import {
  HttpClient,
  HttpClientModule,
  HttpRequest,
  HttpResponse,
  HttpEvent
} from '@angular/common/http';
import { Subscription } from 'rxjs/internal/Subscription';
import { AppConsts } from '@shared/AppConsts';
import { EstimateServiceProxy, CreateEstimateDto, CreateImageDto, StateDto } from '@shared/service-proxies/service-proxies';
import { DecimalPipe } from '@angular/common';
@Component({
  selector: 'app-create-or-update-estimate',
  templateUrl: './create-or-update-estimate.component.html',
  styleUrls: ['./create-or-update-estimate.component.css']
})
export class CreateOrUpdateEstimateComponent extends AppComponentBase implements OnInit {
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

  postUrl = AppConsts.remoteServiceBaseUrl + '/Profile/UploadProfilePicture';
  myFormData: FormData;
  httpEvent: HttpEvent<{}>;

  estimateInput: CreateEstimateDto;
  imageInput: ImageInput[] = [];
  decimalMask: string;
  states: StateDto[] = [];
  // tslint:disable-next-line:no-shadowed-variable
  constructor(injector: Injector, public HttpClient: HttpClient,
    private _estimateServiceProxy: EstimateServiceProxy,
    private _dialogRef: MatDialogRef<CreateOrUpdateEstimateComponent>
  ) {
    super(injector);
    // this.decimalMask = createNumberMask({
    //   prefix: '',
    //   allowDecimal: true,
    //   integerLimit: 10,
    //   autoGroup: true,
    //   digits: 2,
    //   allowLeadingZeroes: true
    // });
  }

  ngOnInit() {
    this.estimateInput = new CreateEstimateDto();
    this.getAllState();
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }

  colorChanged(e: string) {
    this.color = e;
  }

  getDate() {
    return new Date();
  }
  createEstimate() {
    this._estimateServiceProxy.create(this.estimateInput).subscribe(result => {
      this.notify.info(this.l('SavedSuccessfully'));
      this.close(true);
    });
  }
  save() {
    this.uploadFiles(this.files);
  }

  getAllState() {
    this._estimateServiceProxy.getAllState().subscribe(result => {
      this.states = result;
    });
  }
  uploadFiles(files: File[]): Subscription {
    const formData: FormData = new FormData();
    for (let i = 0; i < files.length; i++) {
      formData.append('Image', files[i]);
    }

    const config = new HttpRequest('POST', this.postUrl, formData, {
      reportProgress: true
    });

    return this.HttpClient.request(config)
      .subscribe(event => {
        this.httpEvent = event;
        if (event instanceof HttpResponse) {
          this.imageInput = (event.body as any).result as ImageInput[];
          // tslint:disable-next-line:prefer-const

          const listImage: CreateImageDto[] = [];
          this.imageInput.forEach(element => {
            const imageInput = new CreateImageDto();
            imageInput.imageName = element.fileName;
            imageInput.imageSize = element.size;

            listImage.push(imageInput);
          });
          console.log(listImage);
          this.estimateInput.listFileName = listImage;
          console.log(this.estimateInput);
          this.createEstimate();

        }
      },
        error => {
          alert('!upload fails' + error.toString());
        });
  }
}
export class ImageInput {
  fileName: string;
  size: number;
}
