export const vehicleType = {
  0: "Truck",
  1: "Van"
}
export const speedType = {
  0: "kmph",
  1: "mph"
}
export const temperatureType = {
  0: "Celcius",
  1: "Fahrenheit"
}

export const mphToKmph = mph => {
  return (mph * 1.609344).toFixed(2);
};
export const fahrenheitToCelcius = f => {
  return (((f-32) * 5)/9).toFixed(2);
}