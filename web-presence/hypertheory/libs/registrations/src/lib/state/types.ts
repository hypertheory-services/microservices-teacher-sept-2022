export type RegistrationEntity = {
  id: string;
  courseId: string;
  offeringId: string;
  userId: string;
  status: 'sending' | 'pending' | 'approved' | 'completed' | 'denied';
};

export type RegistrationSummaryItem = RegistrationEntity & {
  title: string;
  startDate: string;
}