import api from './api';
import * as constants from '../constants';
export const ACTION_TYPES = {
  CREATE: "CREATE",
  UPDATE: "UPDATE",
  DELETE: "DELETE",
  FETCH_ALL: "FETCH_ALL"
}

//convert to required datatype
const formatData = (data)=>{
  if (data.temperatureType == 1) {
    data.engineTemperture = constants.fahrenheitToCelcius(data.engineTemperture);
    delete data.temperatureType;
  }

  if (data.speedType == 1) {
    data.speed = constants.mphToKmph(data.speed);
    delete data.speedType;
  }
  data.plateNumber = data.plateNumber.trim();
  return {
    ...data,
    engineTemperture: +data.engineTemperture,
    speed: +data.speed,
    latitude: +data.latitude,
    longitude: +data.longitude,
    type: +data.type,
  }
}

export const fetchall = () => dispatch => {
  //get api call

  return api.vehicle().fetchall()
    .then(response => {
      dispatch({
        type: ACTION_TYPES.FETCH_ALL,
        payload: response.data
      })
    })
    .catch(err => console.log(err));
}

export const create = (data, onSuccess) => dispatch => {
  data = formatData(data)
  api.vehicle().create(data)
      .then(res => {
          dispatch({
              type: ACTION_TYPES.CREATE,
              payload: res.data
          })
          onSuccess()
      })
      .catch(err => console.log(err))
}

export const update = (id, data, onSuccess) => dispatch => {
  data = formatData(data)
  api.vehicle().update(id, data)
      .then(res => {
        let updatedAt = new Date().toISOString();
          dispatch({
              type: ACTION_TYPES.UPDATE,
              payload: { id, ...data,updatedAt }
          })
          onSuccess()
      })
      .catch(err => console.log(err))
}

export const deleteVehicle = (id, onSuccess) => dispatch => {
  api.vehicle().delete(id)
      .then(res => {
          dispatch({
              type: ACTION_TYPES.DELETE,
              payload: id
          })
          onSuccess()
      })
      .catch(err => console.log(err))
}