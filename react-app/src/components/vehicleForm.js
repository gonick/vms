import React, { useEffect, useState } from 'react';
// import useForm from "./useForm";
import { connect } from 'react-redux';
import * as constants from '../constants';
import { useForm } from 'react-hook-form'
import * as actions from '../actions/vehicle';
import { useToasts } from "react-toast-notifications";

//default form values
const initialFieldValues = {
  plateNumber: '',
  speed: 0,
  speedType: 0,
  latitude: 0,
  longitude: 0,
  engineTemperture: 0,
  temperatureType: 0,
  type: 0
}

const VehicleForm = props => {

  const { register, handleSubmit, errors, reset } = useForm({ defaultValues: initialFieldValues, mode: "onBlur" });
  const { addToast } = useToasts()

  //set form data on component mount hook
  useEffect(() => {
    if (props.id !== 0) {
      reset({ ...initialFieldValues, ...props.vehicleList.find(x => x.id === props.id) });
    }
  }, [props.id])

  //loader state for showing 
  const [showLoader, changeLoaderStatus] = useState(false);

  /**
   * dispatch event to update or create vehicle data after validating
   */
  const onSubmit = data => {
    changeLoaderStatus(true);
    
    //show success message via toast
    const onSuccess = () => {
      addToast("Submitted successfully", { appearance: 'success' });
      changeLoaderStatus(false);
      props.onClose();
    }
    //case for adding 
    if (props.id === -1)
      props.createVehicle(data, onSuccess)
    else
      props.updateVehicle(props.id, data, onSuccess)

  };

  return (
    <form autoComplete="off" noValidate onSubmit={handleSubmit(onSubmit)}>
      <div className="form-group">
        <label htmlFor="plateNumber">License Plate:</label>
        <input type="text" name="plateNumber"
          className="form-control" placeholder="Enter License Plate Number" id="plateNumber" 
          ref={register({ 
            required: { value: true, message: "This field is required" },
            maxLength: { value: 50, message: "License plate number exceeds 50 characters" } })} />
        <span className="error">{errors?.plateNumber?.message}</span>
      </div>
      <div className="form-group">
        <label htmlFor="type">Vehicle Type:</label>
        <select className="form-control" name="type" ref={register({ required: { value: true, message: "This field is required" } })}>
          {Object.keys(constants.vehicleType).map(key => (<option key={key} value={key}>{constants.vehicleType[key]}</option>))}
        </select>
        <span className="error">{errors?.type?.message}</span>
      </div>
      <div className="form-group">
        <label htmlFor="speed">Speed:</label>
        <div className="form-inline">
          <input type="number" name="speed" className="form-control"
            placeholder="Enter Speed" id="speed"
            ref={register({
              required: { value: true, message: "This field is required" },
              min: { value: 0, message: "Speed should be a positive number" },
              max: { value: 500, message: "Speed cannot be greater that 500" }
            })} />
        &nbsp;
        <select name="speedType" ref={register()} className="form-control">
            {Object.keys(constants.speedType).map(key => {
              return (<option key={key} value={key}>{constants.speedType[key]}</option>)
            })}
        </select>
        </div>
        <span className="error">{errors?.speed?.message}</span>
      </div>
      <div className="form-group">
        <label htmlFor="engineTemperture">Engine Temperature:</label>
        <div className="form-inline">
          <input type="number" name="engineTemperture"
            className="form-control" placeholder="Enter Engine Temperate" id="engineTemperture" 
            ref={register({ 
              required: { value: true, message: "This field is required" },
              min: { value: -100, message: "Engine Temperature cannot be below -100" },
              max: { value: 2000, message: "Engine Temperature cannot be more than 2000" }
               })} />
          &nbsp;
            <select name="temperatureType" ref={register()} className="form-control">
            {Object.keys(constants.temperatureType).map(key => {
              return (<option key={key} value={key}>{constants.temperatureType[key]}</option>)
            })}
          </select>
        </div>
        <span className="error">{errors?.engineTemperture?.message}</span>
      </div>
      <div className="form-group">
        <label >Location: (Latitude , Longitude)</label>
        <div className="form-inline">
          <input type="number" name="latitude"
            className="form-control mb-2 mr-sm-2" placeholder="Enter Latitude" id="latitude"
            ref={
              register({
                required: { value: true, message: "This field is required" },
                min: { value: -90, message: "Latitude should be between -90 to 90" },
                max: { value: 90, message: "Latitude should be between -90 to 90" }
              })} /> , &nbsp;
          <input type="number" name="longitude"
            className="form-control mb-2 mr-sm-2" placeholder="Enter Longitude" id="longitude"
            ref={register({
              required: { value: true, message: "This field is required" },
              min: { value: 0, message: "Longitude should be between 0 to 180" },
              max: { value: 180, message: "Longitude should be between 0 to 180" }
            })} />
        </div>
        <span className="error">{errors?.latitude?.message || errors?.longitude?.message}</span>
      </div>

      <div className="modal-footer">
        <button type="submit" className="btn btn-primary" data-dismiss="modal" disabled={showLoader}>
          Save
        {showLoader ? <span className="btn-loader"></span> : ''}
        </button> &nbsp;
        <input type="button" className="btn btn-danger" data-dismiss="modal" onClick={props.onClose} value="Close" />
      </div>
    </form>
  )
}

const mapStateToProps = state => ({
  vehicleList: state.vehicle.list
})
const mapActionToProps = {
  createVehicle: actions.create,
  updateVehicle: actions.update
}
export default connect(mapStateToProps, mapActionToProps)(VehicleForm);