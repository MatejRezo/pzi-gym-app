import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs';
import { getCurrentEnv } from 'src/app/constants/environments';

@Injectable({
  providedIn: 'root'
})
export class ExercisesService {
  apiUrl = getCurrentEnv();

  constructor(private http: HttpClient) { }

  // EXERCISES
  getTrainings() {
    return this.http.get(`${this.apiUrl}/exercise`)
      .pipe(take(1));
  }

  addTraining(training: any) {
    return this.http.post(`${this.apiUrl}/exercise`, training)
      .pipe(take(1));
  }

  deleteTraining(id: number) {
    return this.http.delete(`${this.apiUrl}/exercise/${id}`)
      .pipe(take(1));
  }

  updateTraining(news: any) {
    return this.http.put(`${this.apiUrl}/exercise`, news)
      .pipe(take(1));
  }

  // TRAINING PLANS
  getTrainingPlans() {
    return this.http.get(`${this.apiUrl}/plans`)
      .pipe(take(1));
  }

  addTrainingPlan(payload: any) {
    return this.http.post(`${this.apiUrl}/plans`, payload)
      .pipe(take(1));
  }

  deleteTrainingPlan(id: number) {
    return this.http.delete(`${this.apiUrl}/plans/${id}`)
      .pipe(take(1));
  }

  unlinkAllExercises(planId: number) {
    return this.http.patch(`${this.apiUrl}/plans/unlink-all-exercises?planId=${planId}`, { planId })
      .pipe(take(1));
  }

  linkExercisesToPlan(planId: number, exerciseId: number) {
    return this.http.patch(`${this.apiUrl}/plans/link-exercise?planId=${planId}&exerciseId=${exerciseId}`, { planId, exerciseId })
      .pipe(take(1));
  }
}
