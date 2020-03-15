import api from './api';

export const ACTION_TYPES = {
  CREATE: "CREATE",
  UPDATE: "UPDATE",
  DELETE: "DELETE",
  FETCH_ALL: "FETCH_ALL"
}

const formateData = (data)=>{
  return {
    ...data,
    temperature: +data.temperature,
    speed: +data.speed,
    latitude: +data.latitude,
    longitude: +data.longitude,
    type: +data.type,
  }
}

export const fetchall = () => dispatch => {
  //get api call

  api.vehicle().fetchall()
    .then(response => {
      dispatch({
        type: ACTION_TYPES.FETCH_ALL,
        payload: response.data
      })
    })
    .catch(err => console.log(err));
}

export const create = (data, onSuccess) => dispatch => {
  data = formateData(data)
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
  data = formateData(data)
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