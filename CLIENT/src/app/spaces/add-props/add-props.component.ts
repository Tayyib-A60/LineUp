import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Store } from '@ngrx/store';
import { SpaceService } from '../space.service';
import * as spaceReducer from '../state/space.reducers';
import * as spaceActions from '../state/space.actions';

@Component({
  selector: 'app-add-props',
  templateUrl: './add-props.component.html',
  styleUrls: ['./add-props.component.scss']
})
export class AddPropsComponent implements OnInit {

  amenityForm: FormGroup;
  spaceTypeForm: FormGroup;
  
    constructor(private formBuilder: FormBuilder,
                private store: Store<spaceReducer.AppState>,
                private spaceService: SpaceService) { }

  ngOnInit() {
    this.initializeForm();
  }
  private initializeForm() {
    this.amenityForm = this.formBuilder.group({
      name: ['', Validators.required],
      price: ''
    });

    this.spaceTypeForm = this.formBuilder.group({
      type: ['', Validators.required]
    });
  }

  createSpaceType() {
    // console.log(this.spaceTypeForm.value);
    this.store.dispatch(new spaceActions.CreateSpaceType(this.spaceTypeForm.value));
    this.spaceTypeForm.reset();
  }
  createAmenity() {
    // console.log(this.amenityForm.value);
    this.store.dispatch(new spaceActions.CreateAmenity(this.amenityForm.value));
    this.amenityForm.reset();
  }
}
