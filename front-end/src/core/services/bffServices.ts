import axios, { AxiosResponse, AxiosError } from "axios";

const BASE_URL = "https://seu-bff-url.com";

interface ApiResponse<T> {
  data: T;
}

interface ErrorDetails {
  message: string;
}

const bffServices = {
  post: async <T>(endpoint: string, data: any): Promise<T> => {
    try {
      const response: AxiosResponse<ApiResponse<T>> = await axios.post(
        `${BASE_URL}/${endpoint}`,
        data
      );
      return response.data.data;
    } catch (error: any) {
      throw handleApiError(error);
    }
  },
  put: async <T>(endpoint: string, data: any): Promise<T> => {
    try {
      const response: AxiosResponse<ApiResponse<T>> = await axios.put(
        `${BASE_URL}/${endpoint}`,
        data
      );
      return response.data.data;
    } catch (error: any) {
      throw handleApiError(error);
    }
  },
  get: async <T>(endpoint: string): Promise<T> => {
    try {
      const response: AxiosResponse<ApiResponse<T>> = await axios.get(
        `${BASE_URL}/${endpoint}`
      );
      return response.data.data;
    } catch (error: any) {
      throw handleApiError(error);
    }
  },
  delete: async <T>(endpoint: string): Promise<T> => {
    try {
      const response: AxiosResponse<ApiResponse<T>> = await axios.delete(
        `${BASE_URL}/${endpoint}`
      );
      return response.data.data;
    } catch (error: any) {
      throw handleApiError(error);
    }
  },
};

const handleApiError = (error: AxiosError<ErrorDetails>) => {
  if (error.response) {
    throw new Error(
      `Erro ${error.response.status}: ${error.response.data.message}`
    );
  } else if (error.request) {
    throw new Error("Erro de comunicação com o servidor");
  } else {
    throw new Error(`Erro: ${error.message}`);
  }
};

export default bffServices;
