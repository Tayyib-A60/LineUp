import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { Store, select } from '@ngrx/store';
import * as spaceReducer from '../state/space.reducers';
import * as spaceActions from '../state/space.actions';
import * as spaceSelectors from '../state/space.selector';
import { Params, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { SpaceType } from '../models/spaceType.model';
import { takeWhile } from 'rxjs/operators';
import { Space } from '../models/space.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-addspace',
  templateUrl: './addspace.component.html',
  styleUrls: ['./addspace.component.scss']
})
export class AddspaceComponent implements OnInit {
  amenityItems = [ ];
  selectedSpaceType = '';
  spaceForm: FormGroup;
  id: string;
  editMode = false;
  spaceTypes$: Observable<SpaceType[]>;
  componentActive = true;
  spaceTypes: SpaceType[];
  spaceToEdit = <Space>{};
  currentUser: any;
  latitude = 0;
  longitude = 0;
  infoWindow: any;
  selectedLocation = '';
    constructor(private formBuilder: FormBuilder,
                private store: Store<spaceReducer.SpaceState>,
                private route: ActivatedRoute,
                private httpClient: HttpClient) { }
                
    ngOnInit() {

      navigator.geolocation.getCurrentPosition((myLocation) => {
        this.latitude = myLocation.coords.latitude;
        this.longitude = myLocation.coords.longitude;
      });

      this.currentUser = JSON.parse(localStorage.getItem('currentUser'));

      this.route.params
      .subscribe(
        (params: Params) => {
          this.id = params['id'];
          this.editMode = params['id'] != null;
        });
      if(this.editMode) {
        this.store.dispatch(new spaceActions.GetSingleSpace(Number(this.id)));
        this.store.pipe(select(spaceSelectors.getSingleSpace),
        takeWhile(() => this.componentActive))
        .subscribe(space => {
          this.spaceToEdit = space;
          if(this.spaceToEdit)
            this.intializeFormWtValues();
        });
      }
      this.store.dispatch(new spaceActions.GetSpaceTypes());
      this.store.pipe(select(spaceSelectors.getSpaceTypes),
      takeWhile(() => this.componentActive))
      .subscribe(spaceTypes => {
        this.spaceTypes = spaceTypes;
      });
      if(this.editMode == false)
        this.initializeForm();
    }

    ngOnDestroy(): void {
      this.componentActive = false;
    }

    onLocationSelect(event) {
      this.latitude = event.coords.lat;
      this.longitude = event.coords.lng;
      this.spaceForm.get('locationLong').patchValue(event.coords.lng);
      this.spaceForm.get('locationLat').patchValue(event.coords.lat);
      this.httpClient.get(`https://maps.googleapis.com/maps/api/geocode/json?latlng=${event.coords.lat},
      ${event.coords.lng}&key=AIzaSyDqDa-Jf1KhEOO0FXyJwReGiquRMCaz9Bs`).subscribe(res => {
        const { formatted_address } = res['results'][0];
        console.log(res);
        this.spaceForm.get('locationName').patchValue(formatted_address);
        this.selectedLocation = formatted_address;
        console.log(this.selectedLocation);
      })      
    }

    onPinSelect(event) {
      console.log('Marker clicked', event);
      
    }

    private initializeForm() {
      let amenitiesArray = new FormArray([]);
      let name = '';
      let locationName = '';
      let locationLong = '';
      let locationLat = '';
      let size = null;
      let type = {type: '', id: null};
      let st = '';
      let stid = null;
      let description =  '';
      let pricePH = null;
      let discountPH = null;
      let pricePD = null;
      let discountPD = null;
      let pricePW = null;
      let discountPW = null;
      let currentUserId = this.currentUser['id'];

      this.spaceForm = new FormGroup({
        'name': new FormControl(name, [Validators.required,  Validators.minLength(3),
                                      Validators.maxLength(50)]),
        // 'locationName': new FormControl(locationName, Validators.required),
        // 'locationLong': new FormControl(locationLong, Validators.required),
        // 'locationLat': new FormControl(locationLat, Validators.required),
        'description': new FormControl(description, Validators.required),
        'size': new FormControl(size, Validators.required),
        // 'pricePH': new FormControl(pricePH, Validators.required),
        // 'discountPH': new FormControl(discountPH, Validators.required),
        // 'pricePD': new FormControl(pricePD, Validators.required),
        // 'discountPD': new FormControl(discountPD, Validators.required),
        // 'pricePW': new FormControl(pricePW, Validators.required),
        // 'discountPW': new FormControl(discountPW, Validators.required),
        // 'amenities': amenitiesArray,
        'userId': new FormControl(currentUserId),
        'type': new FormGroup({
          'type': new FormControl(null),
          'id': new FormControl(null)
        })
      });
    }

    private intializeFormWtValues() {
      // console.log(this.spaceToEdit);
      
      if(this.editMode) { // && this.spaceToEdit
        // fetch the space to edit nd initialize the form to contain it's properties here.
        let amenitiesArray = new FormArray([]);
        let name = '';
        let locationName = this.selectedLocation;
        let locationLong = this.longitude.toString();
        let locationLat = this.latitude.toString();
        let size = null;
        let type = {type: '', id: null};
        let st = '';
        let stid = null;
        let description =  '';
        let pricePH = null;
        let discountPH = null;
        let pricePD = null;
        let discountPD = null;
        let pricePW = null;
        let discountPW = null;
        let currentUserId = this.currentUser['id'];
            
            // this.spaceToEdit = space;
        name = this.spaceToEdit.name;
        if(this.spaceToEdit.location) {
          locationName = this.spaceToEdit.location.name;
          locationLong = this.spaceToEdit.location.long;
          locationLat = this.spaceToEdit.location.lat;
        }
        if(this.spaceToEdit.pricePD) {
          pricePH = this.spaceToEdit.pricePH.price;
          discountPH = this.spaceToEdit.pricePH.discount;
          pricePD = this.spaceToEdit.pricePD.price;
          discountPD = this.spaceToEdit.pricePD.discount;
          pricePW = this.spaceToEdit.pricePW.price;
          discountPW = this.spaceToEdit.pricePW.discount;
        }
        description = this.spaceToEdit.description;
        size = this.spaceToEdit.size;
        type = this.spaceToEdit.type;
        if(this.spaceToEdit.type) {
          st = this.spaceToEdit.type.type;
          stid = this.spaceToEdit.type.id;
        }
        this.selectedSpaceType = st;
        const amenities = this.spaceToEdit.amenities;
        // console.log(amenities);
          
        if(amenities) {                
            amenities.map(a => {
              amenitiesArray.push(
                new FormGroup({
                  'name': new FormControl(a['name'], Validators.required),
                  'price': new FormControl(a['price'], Validators.required),
                  // 'id': new FormControl(a['id'])
                })
              );
            }); 
        } 
            
        this.spaceForm = new FormGroup({
          'id': new FormControl(this.id, Validators.required),
          'name': new FormControl(name, [Validators.required,  Validators.minLength(3),
                                        Validators.maxLength(50)]),
          'locationName': new FormControl(locationName, Validators.required),
          'locationLong': new FormControl(locationLong, Validators.required),
          'locationLat': new FormControl(locationLat, Validators.required),
          'description': new FormControl(description, Validators.required),
          'size': new FormControl(size, Validators.required),
          'pricePH': new FormControl(pricePH, Validators.required),
          'discountPH': new FormControl(discountPH, Validators.required),
          'pricePD': new FormControl(pricePD, Validators.required),
          'discountPD': new FormControl(discountPD, Validators.required),
          'pricePW': new FormControl(pricePW, Validators.required),
          'discountPW': new FormControl(discountPW, Validators.required),
          'amenities': amenitiesArray,
          'userId': new FormControl(currentUserId),
          'type': new FormGroup({
            'type': new FormControl(st),
            'id': new FormControl(stid)
          })
        });        
      }
      // this.spaceForm = this.formBuilder.group({
      //   spaceName: ['', [Validators.required,
      //         Validators.minLength(3),
      //         Validators.maxLength(50)]],
      //   location: ['', Validators.required],
      //   size: '',
      //   description: '',
      //   price: '',
      //   amenities
      // });
    }

    addAmenity() {
      (<FormArray>this.spaceForm.get('amenities')).push(
        new FormGroup({
          'name': new FormControl(null, Validators.required),
          'price': new FormControl(null, Validators.required)
        })
      );      
    }
    
    removeAmenity(index: number) {
      (<FormArray>this.spaceForm.get('amenities')).removeAt(index);
    }

    toggleCheckbox(item) {
      if(item.currentTarget.checked) {
        this.amenityItems.push(item.currentTarget.id);
      } else {
        this.amenityItems.splice((this.amenityItems.indexOf(item.currentTarget.id),1))
      }
    }

    createSpace() {
      const spaceTypeIndex = this.spaceTypes.findIndex(c => c.type === this.selectedSpaceType);
      const spaceTypeId = this.spaceTypes[spaceTypeIndex].id;
      this.spaceForm.get('type').patchValue({
        type: this.selectedSpaceType,
        id: spaceTypeId
      });
      if(this.editMode) {
        // console.log(this.spaceForm.value);
        const spaceToUpdate = {
          id: Number(this.id),
          userId: this.currentUser.id,
          type: {
            type: this.selectedSpaceType,
            id: spaceTypeId
          },
          name: this.spaceForm.controls['name'].value,
          location: {
            id: this.spaceToEdit.location?this.spaceToEdit.location.id : 0,
            name: this.spaceForm.controls['locationName'].value,
            long: this.spaceForm.controls['locationLong'].value,
            lat: this.spaceForm.controls['locationLat'].value
          },
          pricePH: {
            id: this.spaceToEdit.pricePH? this.spaceToEdit.pricePH.id: 0,
            price: this.spaceForm.controls['pricePH'].value,
            discount: this.spaceForm.controls['discountPH'].value
          },
          pricePD: {
            id: this.spaceToEdit.pricePD? this.spaceToEdit.pricePD.id: 0,
            price: this.spaceForm.controls['pricePD'].value,
            discount: this.spaceForm.controls['discountPD'].value
          },
          pricePW: {
            id: this.spaceToEdit.pricePW? this.spaceToEdit.pricePW.id : 0,
            price: this.spaceForm.controls['pricePW'].value,
            discount: this.spaceForm.controls['discountPW'].value
          },
          description: this.spaceForm.controls['description'].value,
          size: this.spaceForm.controls['size'].value,
          amenities: [...this.spaceForm.controls['amenities'].value]
        }
        // console.log(spaceToUpdate);
        
        this.store.dispatch(new spaceActions.UpdateSpace(<Space>(spaceToUpdate)));
      }
      else {
        const spaceToCreate = {
          userId: this.currentUser.id,
          type: {
            type: this.selectedSpaceType,
            id: spaceTypeId
          },
          name: this.spaceForm.controls['name'].value,
          // location: {
          //   name: this.spaceForm.controls['locationName'].value,
          //   long: this.spaceForm.controls['locationLong'].value,
          //   lat: this.spaceForm.controls['locationLat'].value
          // },
          // pricePH: {
          //   price: this.spaceForm.controls['pricePH'].value,
          //   discount: this.spaceForm.controls['discountPH'].value
          // },
          // pricePD: {
          //   price: this.spaceForm.controls['pricePD'].value,
          //   discount: this.spaceForm.controls['discountPD'].value
          // },
          // pricePW: {
          //   price: this.spaceForm.controls['pricePW'].value,
          //   discount: this.spaceForm.controls['discountPW'].value
          // },
          description: this.spaceForm.controls['description'].value,
          size: this.spaceForm.controls['size'].value

        }
        this.store.dispatch(new spaceActions.CreateSpace(<Space>(spaceToCreate)));
      }
      // console.log(this.spaceForm.value);
      this.spaceForm.reset();
      this.selectedSpaceType = '';  
    }

}
