import {IPaginationRequest} from "@core/dtos";

export function getDefaultPaginationRequest(size: number = 25): IPaginationRequest {
  return {
    pageNumber: 1,
    pageSize: size
  }
}
