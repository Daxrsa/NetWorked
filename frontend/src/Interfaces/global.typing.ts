export interface ICompany {
    id: string
    name: string
    size: string
    cityLocation: string
    logo: string
}

export interface ICreateCompanyDto {
    name: string
    size: string
    cityLocation: string
    file: File
}

export interface IJob {
    id: string
    title: string
    description: string
    requirements: string
    jobCategory: string
    jobLevel: string
    companyName: string
    createdAt: string
    imgPath: string
}

export interface ICreateJob {
    title: string
    description: string
    requirements: string
    categoryId: string
    jobLevel: string
    companyId: string
}

export interface IApplication {
    id: string
    applicantId: string
    dateApplied: string
    resumeUrl: string
    jobId: int
    jobTitle: string
}

export interface ICreateApplicationDto {
    applicantId: string
    jobId: int
}

export interface ICategory {
    id: string
    name: string
}