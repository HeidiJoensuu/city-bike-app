import axios from "axios"
import { config } from '../utils/config'

const headers = {
  'Content-Type': 'application/json, text/plain'
}

/**
 * Fetch list of stations from api
 * @param {Number} data.offset
 * @param {Number} data.limit 
 * @param {String} data.order
 * @param {String} data.search 
 * @param {Boolean} data.desc 
 * @param {Number} data.month 
 * @returns {Object} Response from api
 */
export const getStations = async (data) => {
  const url = `${config.url}Station?offset=${data.offset}&limit=${data.limit}&order=${data.order}&${data.search ? `search=${data.search}&` : ``}descending=${data.desc}`
  const response = await axios.get(url)
  return response.data
}

/**
 * Fetch information of individual station from api
 * @param {Number} data.id
 * @param {Number} data.month 
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

export const getNumberOfStations = async (data) => {
  const url = `${config.url}Station/count${data.search ? `?search=${data.search}` : ``}`
  const response = await axios.get(url)
  return response.data
}

export const getStationsNames = async () => {
  const response = await axios.get(`${config.url}Station/names`)
  return response.data
}