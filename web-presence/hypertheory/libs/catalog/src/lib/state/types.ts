export type Course = {
  id: string;
  title: string;
  category: 'Frontend' | 'Services';
  description: string;
  offerings: CourseOffering[];
};
export type CourseOffering = {
  id: string;
  startDate: string;
  endDate: string;
  numberOfDays: number;
  price: number;
  location: string;
};

export type CourseOfferingDetails = Omit<Course, 'offerings'> &
  Omit<CourseOffering, 'id'>;
