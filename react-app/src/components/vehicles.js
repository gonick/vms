
import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import * as actions from '../actions/vehicle';
import { vehicleType } from '../constants';
import Modal from './modal';
import VehicleForm from './vehicleForm';
import { useToasts } from "react-toast-notifications";

const Vehicles = props => {


  //maintain popup visibility state
  const [showPopup, setPopup] = useState(0);
  const [showLoader, setLoader] = useState(true);

  // fetch all vehicles in the system using react hook
  useEffect(() => {
    const init = async () => {
      await props.fetchAllVehicles();
      setLoader(false);
    }
    init();
  }, [])

  const onClose = () => {
    setPopup(0);
  }

  const { addToast } = useToasts();

  const onDelete = id => {
    //Todo: maintain static text in string constatnt file
    if (window.confirm('Are you sure to delete this record?'))
      props.delete(id, () => addToast("Deleted successfully", { appearance: 'info' }))
  }

  const tableView = ()=>{
    return (props.vehicleList.map((vehicle, index) => {
      return (
        <tr key={vehicle.id}>
          <td>{index + 1}</td>
          <td>{vehicle.plateNumber}</td>
          <td>{vehicleType[vehicle.type]}</td>
          <td>{vehicle.speed} Kmph</td>
          <td>{vehicle.engineTemperture} &#176;C</td>
          <td>
            <a target="_blank"
              href={`http://maps.google.com/maps?q=${vehicle.latitude},${vehicle.longitude}&ll=${vehicle.latitude},${vehicle.longitude}&z=14`}>
              view</a>
          </td>
          <td>{new Date(vehicle.updatedAt).toDateString()}</td>
          <td>
            <button className="btn btn-sm btn-secondary" onClick={() => { setPopup(vehicle.id) }}>Edit</button>
      &nbsp;
      <button className="btn btn-sm btn-danger" onClick={() => { onDelete(vehicle.id) }}>Delete</button>
          </td>
        </tr>
      )
    }))
  }

  const loadingView = ()=>{
    return (<tr>
      <td colSpan="8"> Loading...</td>
    </tr>)
  }

  const noDataFoundView = ()=>{
    return (<tr>
      <td colSpan="8"> No Vehicles Available</td>
    </tr>)
  }
  return (
    <div className="container">
      <div className="row col-12 m-0 p-0">
        <h2 className="col-6 p-0 mt-3 mb-4">Vehicle List</h2>
        <div className="col-6 p-0 mt-4 mb-4 text-right">
          <button className="btn btn-sm btn-primary" onClick={() => { setPopup(-1) }}>Add Vehicle</button>
        </div>
      </div>
      <table className="table table-bordered table-hover">
        <thead className="thead-light">
          <tr>
            <th scope="col">#</th>
            <th scope="col">License Plate</th>
            <th scope="col">Vehicle Type</th>
            <th scope="col">Speed</th>
            <th scope="col">Engine Temp</th>
            <th scope="col">Location</th>
            <th scope="col">Last Updated</th>
            <th scope="col">Actions</th>
          </tr>
        </thead>
        <tbody>
          {
            !showLoader ? (props.vehicleList?.length ? tableView() : noDataFoundView()) : loadingView()
          }
        </tbody>
      </table>

      {
        showPopup ? (
          <Modal onClose={onClose} >
            <VehicleForm id={showPopup} onClose={onClose} />
          </Modal>
        ) : null
      }
    </div>
  )
}

const mapStateToProps = state => ({
  vehicleList: state.vehicle.list
})

const mapActionToProps = {
  fetchAllVehicles: actions.fetchall,
  delete: actions.deleteVehicle

}

export default connect(mapStateToProps, mapActionToProps)(Vehicles);