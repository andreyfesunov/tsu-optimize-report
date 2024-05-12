import {IPaginationRequest} from "@core/dtos";

export function getDefaultPaginationRequest(): IPaginationRequest {
  return {
    pageNumber: 1,
    pageSize: 25
  }
}
