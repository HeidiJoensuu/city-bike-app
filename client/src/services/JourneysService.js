import axios from "axios"
import { config } from '../utils/config'

/**
 * Fetch list of journeys from api
 * @param {Object} data {offset: Number, limit: Number, order: String, Descending: Boolean, month: Number}
 * (optionals):{search: String, departure: String, returntime: String, distanceMin: Number, distanceMax: Number, durationMin: Number, durationMax: Number}
 * @returns {Object} Response from api
 */
export const getJourneys = async (data) => {
  let url = `${config.url}Journeys?offset=${data.offset}&limit=${data.limit}&order=${data.order}&descending=${data.desc}&month=${data.month}`
  url += `${data.search ? `&search=${data.search}` : ``}`
  url += `${data.departure ? `&departure=${data.departure.slice(0, data.departure.indexOf('+'))}` : ``}`
  url += `${data.returntime ? `&returntime=${data.returntime.slice(0, data.returntime.indexOf('+'))}` : ``}`
  url += `${data.distanceMin ? `&distanceMin=${data.distanceMin}` : ``}`
  url += `${data.distanceMax ? `&distanceMax=${data.distanceMax}` : ``}`
  url += `${data.durationMin ? `&durationMin=${data.durationMin}` : ``}`
  url += `${data.durationMax ? `&durationMax=${data.durationMax}` : ``}`
  const response = await axios.get(url)
  return response.data
}

/**
 * Posts new journey to api
 * @param {Object} data new journey
 * @param {Date} data.departure
 * @param {Date} data.returntime
 * @param {Number} data.departure_station_id
 * @param {String} data.departure_station_name
 * @param {Number} data.return_station_id
 * @param {String} data.return_station_name
 * @param {Number} data.covered_distance_m
 * @param {Number} data.duration_sec
 * @returns {Object} Response from api
 */
export const postJourney = async (data) => {
  const response = await axios.post(`${config.url}Journeys`, data)
  return response.data
}

/**
 * Fetch count of journeys from api
 * @param {Object} data {month: Number} (optionals):{search: String, departure: String, returntime: String, distanceMin: Number, distanceMax: Number, durationMin: Number, durationMax: Number}
 * @returns {Object} Response from api
 */
export const getJourneysCounted =async (data) => {
  let url = `${config.url}Journeys/count?month=${data.month}`
  url += `${data.search ? `&search=${data.search}` : ``}`
  url += `${data.departure ? `&departure=${data.departure.slice(0, data.departure.indexOf('+'))}` : ``}`
  url += `${data.returntime ? `&returntime=${data.returntime.slice(0, data.returntime.indexOf('+'))}` : ``}`
  url += `${data.distanceMin ? `&distanceMin=${data.distanceMin}` : ``}`
  url += `${data.distanceMax ? `&distanceMax=${data.distanceMax}` : ``}`
  url += `${data.durationMin ? `&durationMin=${data.durationMin}` : ``}`
  url += `${data.durationMax ? `&durationMax=${data.durationMax}` : ``}`
  const response = await axios.get(url)
  return response.data
}