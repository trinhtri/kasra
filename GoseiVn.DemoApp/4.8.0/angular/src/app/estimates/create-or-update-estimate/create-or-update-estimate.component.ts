import { Component, OnInit, Injector, Optional, Inject } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
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
import createNumberMask from 'text-mask-addons/dist/createNumberMask';
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
  viewImageUrl = AppConsts.remoteServiceBaseUrl + '/Home/Image/';

  myFormData: FormData;
  httpEvent: HttpEvent<{}>;

  estimateInput: CreateEstimateDto = new CreateEstimateDto();
  imageInput: ImageInput[] = [];
  decimalMask: string;
  states: StateDto[] = [];
  images: any[] = [];

  // tslint:disable-next-line:no-shadowed-variable
  constructor(injector: Injector, public HttpClient: HttpClient,
    private _estimateServiceProxy: EstimateServiceProxy,
    private _dialogRef: MatDialogRef<CreateOrUpdateEstimateComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
  ) {
    super(injector);
  }

  ngOnInit() {
    if (this._id) {
      this._estimateServiceProxy.getDataForEdit(this._id).subscribe(result => {
        this.estimateInput = result;
        if (this.estimateInput.listFileName.length > 0) {
          for (let i = 0; i < this.estimateInput.listFileName.length; i++) {
            this.images.push({
              src: (this.viewImageUrl + this.estimateInput.listFileName[i].id.toString()),
              fileName: this.estimateInput.listFileName[i].imageName
            });
          }
        }
      });
    }
    console.log(this._id);
    this.decimalMask = createNumberMask({
      prefix: '',
      allowDecimal: true,
      integerLimit: 10,
      autoGroup: true,
      digits: 2,
      allowLeadingZeroes: true
    });
    this.getAllState();
  }

  close(result: any): void {
    this.estimateInput = new CreateEstimateDto();
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
            const imageDto = new CreateImageDto();
            imageDto.imageName = element.fileName;
            imageDto.imageUrl = element.fileUrl;
            imageDto.imageSize = element.size;

            listImage.push(imageDto);
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
  keyPress(event: any) {
    const pattern = /[0-9]/;

    const inputChar = String.fromCharCode(event.charCode);
    if (event.keyCode !== 8 && !pattern.test(inputChar)) {
      event.preventDefault();
    }
  }

  Calculate() {
    const workHours = +this.estimateInput.workHours.toString().split(',').join('').toString();
    const rate = +this.estimateInput.rate.toString().split(',').join('').toString();
    this.estimateInput.totalAmount = workHours * rate;
  }
}
export class ImageInput {
  fileName: string;
  fileUrl: string;
  size: number;
}
