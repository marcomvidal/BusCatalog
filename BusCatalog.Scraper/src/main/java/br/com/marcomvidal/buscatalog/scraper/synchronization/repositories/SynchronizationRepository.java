package br.com.marcomvidal.buscatalog.scraper.synchronization.repositories;

import org.springframework.data.jpa.repository.JpaRepository;

import br.com.marcomvidal.buscatalog.scraper.synchronization.entities.Synchronization;

public interface SynchronizationRepository extends JpaRepository<Synchronization, Long> {}
