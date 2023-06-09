import axios from "axios"
import { config } from '../utils/config'

/**
 * Fetch list of stations from api
 * @param {Object} data {offset: Number, limit: Number, order: String, Descending: Boolean, month: Number, (optional) search: String}
 * @returns {Object} Response from api
 */
export const getStations = async (data) => {
  const url = `${config.url}Station?offset=${data.offset}&limit=${data.limit}&order=${data.order}&${data.search ? `search=${data.search}&` : ``}descending=${data.desc}`
  const response = await axios.get(url)
  return response.data
}

/**
 * Fetch information of individual station from api
 * @param {Number} data {id: Number, month: Number}
 * @returns {Object} Response from api
 */
export const getStationInfo = async (data) => {
  const url = `${config.url}Station/${data.id}${data.month ? `?month=${data.month}` : ``}`
  const response = await axios.get(url)
  return response.data
}

/**
 * Post new station to api
 * @param {String} data.nimi 
 * @param {String} data.namn
 * @param {String} data.name
 * @param {String} data.osoite
 * @param {String} data.adress
 * @param {Number} data.x
 * @param {Number} data.y
 * @param {Number} data.kapasiteet
 * @returns {Object} Response from api
 */
export const postStation = async (data) => {
  const url = `${config.url}Station`
  const response = await axios.post(url, data)
  return response.data
}

/**
 * Fetch count of stations
 * @param {Object} data {(optional)search: String}
 * @returns {Object} Response from api
 */
export const getNumberOfStations = async (data) => {
  const url = `${config.url}Station/count${data.search ? `?search=${data.search}` : ``}`
  const response = await axios.get(url)
  return response.data
}

/**
 * Fetches all station names
 * @returns {Object} Response from api
 */
export const getStationsNames = async () => {
  const response = await axios.get(`${config.url}Station/names`)
  return response.data
}