import axios from "axios"
import { config } from '../utils/config'

/**
 * Fetch list of journeys from api
 * @param {Number} data.offset
 * @param {Number} data.limit 
 * @param {String} data.order
 * @param {String} data.search 
 * @param {Boolean} data.desc 
 * @param {Number} data.month 
 * @returns {Object} Response from api
 */
export const getJourneys = async (data) => {
  let url = `${config.url}Journeys?offset=${data.offset}&limit=${data.limit}&order=${data.order}&descending=${data.desc}&month=${data.month}`
  url += `${data.search ? `&search=${data.search}` : ``}`
  url += `${data.departure ? `&departure=${data.departure}` : ``}`
  url += `${data.return ? `&return=${data.return}` : ``}`
  url += `${data.distanceMin ? `&distanceMin=${data.distanceMin}` : ``}`
  url += `${data.distanceMax ? `&distanceMax=${data.distanceMax}` : ``}`
  url += `${data.durationMin ? `&durationMin=${data.durationMin}` : ``}`
  url += `${data.durationMax ? `&durationMax=${data.durationMax}` : ``}`
  const response = await axios.get(url)
  return response.data
}

/**
 * Posts new journey to api
 * @param {Date} data.departure
 * @param {Date} data.return
 * @param {Number} data.departure_station_id
 * @param {String} data.departure_station_name
 * @param {Number} data.return_station_id
 * @param {String} data.return_station_name
 * @param {Number} data.covered_distance_m
 * @param {Number} data.duration_sec
 * @returns {Object} Response from api
 */
export const postJourney = async (data) => {
  const templateData = {
      "id": 0,
      "departure": new Date("2021-05-09T08:13:41.676Z"),
      "return": new Date("2021-05-09T08:17:41.676Z"),
      "departure_station_id": 579,
      "departure_station_name": "Niittymaa",
      "return_station_id": 639,
      "return_station_name": "Itäportti",
      "covered_distance_m": 1400,
      "duration_sec": 300
  }
  const response = await axios.post(`${config.url}Journeys`, templateData)
  return response.data
}

export const getJourneysCounted =async (data) => {
  let url = `${config.url}Journeys/count?month=${data.month}`
  url += `${data.search ? `&search=${data.search}` : ``}`
  url += `${data.departure ? `&departure=${data.departure}` : ``}`
  url += `${data.return ? `&return=${data.return}` : ``}`
  url += `${data.distanceMin ? `&distanceMin=${data.distanceMin}` : ``}`
  url += `${data.distanceMax ? `&distanceMax=${data.distanceMax}` : ``}`
  url += `${data.durationMin ? `&durationMin=${data.durationMin}` : ``}`
  url += `${data.durationMax ? `&durationMax=${data.durationMax}` : ``}`
  const response = await axios.get(url)
  return response.data
}