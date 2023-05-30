import { strings } from "./localization"
import dayjs from "dayjs"

export const validateIsItNumber = (value) => {
  try {
    if (value === "") return ""
    if (typeof value !== "number")
      if (!Number.isFinite(Number(value.replace(",", "."))) || Number(value.replace(",", "."))<0) return strings.errors.invalidInput
    return ""
  } catch (error) {
    console.log(error);
  }
}

export const validateNumberIsBiggerThanMinumum = (min, max) => {
  try {
    if (typeof min !== "number" && typeof max !== "number") {
      if (min === "" || max === "") return ""
      if (Number(min.replace(",", ".")) > Number(max.replace(",", "."))) return strings.errors.tooSmallNumber
    }
    return ""
  } catch (error) {
    console.log(error);
  }
}

export const validateIsItIntegerNumber = (value) => {
  try {
    if (!Number.isInteger(Number(value)) || Number(value)<0) return strings.errors.invalidInput
    return ""
  } catch (error) {
    console.log(error);
  }
  
}

export const validateTimeNumberIsBiggerThanMinumum = (minhour, minmin, maxhour, maxmin) => {
  try {
    const min = Number(minhour)*60+Number(minmin)
    const max = Number(maxhour)*60+Number(maxmin)
    if (min === 0 || max === 0) return ""
    if (min > max) return strings.errors.tooSmallNumber
    return ""
  } catch (error) {
    console.log(error);
  }
}

export const convertToNumber = (value) => {
  try{
    return Number(value.replace(",", "."))
  } catch (error) {
    console.log(error)
  }
  
}

export const validateAll = (month,days, departureValue, returnValue, minDist, maxDist,minhour, minmin, maxhour, maxmin) => {
  try {
    if (validateIsItNumber(minDist)) return true
    if (validateIsItNumber(maxDist)) return true
    if (validateIsItIntegerNumber(minhour)) return true
    if (validateIsItIntegerNumber(minmin)) return true
    if (validateIsItIntegerNumber(maxhour)) return true
    if (validateIsItIntegerNumber(maxmin)) return true

    if (departureValue !== "") {
      if (departureValue.isBefore(dayjs(`2021-0${month}-01T00:00`))) return true
      if (departureValue.isAfter(dayjs(`2021-0${month}-${days}T23:59`))) return true
    }
    if (returnValue !== "") {
      if (returnValue.isBefore(dayjs(`2021-0${month}-01T00:00`))) return true
      if (returnValue.isAfter(dayjs(`2021-12-31T23:59:59`))) return true
    }

    if (departureValue !== "" && returnValue !== ""){
      if(departureValue.isAfter(returnValue)) return true
    }
    if (minDist !== "" && maxDist !== "") {
      if (validateNumberIsBiggerThanMinumum(minDist, maxDist)) return true
    }
    if (validateTimeNumberIsBiggerThanMinumum(minhour, minmin, maxhour, maxmin)) return true
    return false
  } catch (error) {
    console.log(error)
  }
}