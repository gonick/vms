
import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import * as actions from '../actions/vehicle';
import { vehicleType } from '../constants';
import Modal from './modal';
import VehicleForm from './vehicleForm';
import { useToasts } from "react-toast-notifications";

const Vehicles = props => {
  useEffect(() => {
    props.fetchAllVehicles();
  }, [])

  const [showPopup, setPopup] = useState(0);

  const onClose = () => {
    setPopup(0);
  }
  const { addToast } = useToasts()
  const onDelete = id => {
    if (window.confirm('Are you sure to delete this record?'))
      props.delete(id, () => addToast("Deleted successfully", { appearance: 'info' }))
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
            {props.vehicleList.map((vehicle, index) => {
              return (
                <tr key={vehicle.id}>
                  <td>{index + 1}</td>
                  <td>{vehicle.licenseNumber}</td>
                  <td>{vehicleType[vehicle.type]}</td>
                  <td>{vehicle.speed} Kmph</td>
                  <td>{vehicle.temperature} &#176;C</td>
                  <td>
                    <a target="_blank"
                      href={`http://maps.google.com/maps?q=${vehicle.latitude},${vehicle.longitude}&ll=${vehicle.latitude},${vehicle.longitude}&z=14`}>
                      view</a>
                  </td>
                  <td>{new Date(vehicle.updatedAt).toLocaleString()}</td>
                  <td>
                    <button className="btn btn-sm btn-secondary" onClick={() => { setPopup(vehicle.id) }}>Edit</button>
                  &nbsp;
                  <button className="btn btn-sm btn-danger" onClick={() => { onDelete(vehicle.id)}}>Delete</button>
                  </td>
                </tr>
              )
            })}
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