import axios from "axios";

// const baseUrl = "https://localhost:5001/api/";
const baseUrl = "https://vms-webapi.azurewebsites.net/api/";

export default {
  vehicle(url = baseUrl + 'Vms/') {
    return {
      fetchall: () => axios.get(url),
      fetchById: id => axios.get(url + id),
      create: vehicle => axios.post(url, vehicle),
      update: (id, vehicle) => axios.put(url + id, vehicle),
      delete: id => axios.delete(url + id)
    }
  }
}